import requests
import uuid
import time
from datetime import datetime, timezone
import urllib3

urllib3.disable_warnings(urllib3.exceptions.InsecureRequestWarning)

# ==============================
# Configuration
# ==============================

ENDPOINT = "https://localhost:7208/Payment/payments"

NUMBER_OF_REQUESTS = 50
REQUEST_RATE_SECONDS = 0.001


# ==============================
# Send requests
# ==============================

total_start = time.perf_counter()

for i in range(1, NUMBER_OF_REQUESTS + 1):

    payment = {
        "paymentId": str(uuid.uuid4()),
        "name": "My Payment",
        "value": 100,
        "submissionDate": datetime.now(timezone.utc).isoformat()
    }

    start = time.perf_counter()

    try:
        response = requests.post(
            ENDPOINT,
            json=payment,
            verify=False
        )

        elapsed = time.perf_counter() - start

        print(
            f"Request {i}/{NUMBER_OF_REQUESTS} | "
            f"PaymentId: {payment['paymentId']} | "
            f"Status: {response.status_code} | "
            f"Time: {elapsed:.3f}s"
        )

    except requests.RequestException as e:
        elapsed = time.perf_counter() - start

        print(
            f"Request {i}/{NUMBER_OF_REQUESTS} | "
            f"FAILED | "
            f"Time: {elapsed:.3f}s | "
            f"Error: {e}"
        )

    if i < NUMBER_OF_REQUESTS:
        time.sleep(REQUEST_RATE_SECONDS)


total_elapsed = time.perf_counter() - total_start

print()
print("==============================")
print(f"Total requests: {NUMBER_OF_REQUESTS}")
print(f"Total time:     {total_elapsed:.3f}s")
print("==============================")