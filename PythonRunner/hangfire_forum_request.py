import argparse
import time
import requests

def main():
    parser = argparse.ArgumentParser(description="Continuously send payment POST requests at a controlled rate.")
    parser.add_argument("--url", required=True, help="API POST endpoint")
    parser.add_argument("--rate", type=float, required=True, help="Requests per second")
    parser.add_argument("--count", type=int, required=True, help="Total number of requests")
    parser.add_argument("--name", default="Forum Demo Customer", help="Payment name")
    parser.add_argument("--amount", type=float, default=100.00, help="Payment amount")
    parser.add_argument("--timeout", type=float, default=10, help="HTTP timeout in seconds")
    args = parser.parse_args()

    if args.rate <= 0:
        raise ValueError("--rate must be greater than 0")
    if args.count <= 0:
        raise ValueError("--count must be greater than 0")

    interval = 1.0 / args.rate

    print(f"Sending {args.count} requests to {args.url}")
    print(f"Rate: {args.rate:g} requests/sec ({interval:.3f}s between requests)")
    print()

    successful = 0
    failed = 0

    start = time.perf_counter()

    for i in range(args.count):
        request_start = time.perf_counter()

        payload = {
            "name": args.name,
            "amount": args.amount,
            "date": time.strftime("%Y-%m-%dT%H:%M:%S")
        }

        try:
            response = requests.post(
                args.url,
                json=payload,
                timeout=args.timeout
            )

            if 200 <= response.status_code < 300:
                successful += 1
                result = "OK"
            else:
                failed += 1
                result = f"HTTP {response.status_code}"

            print(
                f"[{i + 1:>5}/{args.count}] "
                f"{result:<12} "
                f"amount={args.amount:.2f}"
            )

        except requests.RequestException as exc:
            failed += 1
            print(f"[{i + 1:>5}/{args.count}] ERROR       {exc}")

        # Keep the request *start times* approximately rate-limited.
        next_request = start + (i + 1) * interval
        sleep_for = next_request - time.perf_counter()

        if sleep_for > 0:
            time.sleep(sleep_for)

    elapsed = time.perf_counter() - start

    print()
    print("Completed")
    print(f"Successful: {successful}")
    print(f"Failed:     {failed}")
    print(f"Elapsed:    {elapsed:.2f}s")
    print(f"Actual rate: {args.count / elapsed:.2f} requests/sec")


if __name__ == "__main__":
    main()
