import time
import requests
from datetime import datetime

# ==============================
# Configuration
# ==============================

API_URL = "http://localhost:5000/api/payments"

REQUESTS_PER_SECOND = 5
TOTAL_REQUESTS = 100

PAYMENT_NAME = "Forum Demo Customer"
PAYMENT_AMOUNT = 100.00

# ==============================
# Request generation
# ==============================

interval = 1 / REQUESTS_PER_SECOND

successful = 0
failed = 0

print(f"Sending {TOTAL_REQUESTS} requests")
print(f"Rate: {REQUESTS_PER_SECOND} requests/second")
print(f"Endpoint: {API_URL}")
print()

start_time = time.perf_counter()

for i in range(TOTAL_REQUESTS):

    payload = {
        "name": PAYMENT_NAME,
        "amount": PAYMENT_AMOUNT,
        "date": datetime.now().isoformat()
    }

    try:
        response = requests.post(
            API_URL,
            json=payload,
            timeout=10
        )

        if 200 <= response.status_code < 300:
            successful += 1
            status = "OK"
        else:
            failed += 1
            status = f"HTTP {response.status_code}"

        print(
            f"[{i + 1}/{TOTAL_REQUESTS}] "
            f"{status} - "
            f"{payload['amount']:.2f}"
        )

    except requests.RequestException as e:
        failed += 1
        print(f"[{i + 1}/{TOTAL_REQUESTS}] ERROR - {e}")

    # Maintain the requested rate
    target_time = start_time + ((i + 1) * interval)
    sleep_time = target_time - time.perf_counter()

    if sleep_time > 0:
        time.sleep(sleep_time)


elapsed = time.perf_counter() - start_time

print()
print("========== Complete ==========")
print(f"Successful : {successful}")
print(f"Failed     : {failed}")
print(f"Elapsed    : {elapsed:.2f}s")
print(f"Actual rate: {TOTAL_REQUESTS / elapsed:.2f} requests/sec")