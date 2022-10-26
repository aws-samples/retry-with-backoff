## My Project

### Intent

Retry pattern improves application stability by transparently retrying operations that fail due to transient errors. 

### Motivation

In distributed architectures, transient errors may be caused due to service throttling, temporary loss of network connectivity and temporary service unavailability. When the application automatically retries the operations that fail transparently, it improves the user experience and application resilience.  However, frequent retries can overload the network bandwidth resulting in contention. Exponential backoff can be used where retries are done with increasing wait times for a specified number of retry attempts. 



## Security

See [CONTRIBUTING](CONTRIBUTING.md#security-issue-notifications) for more information.

## License

This library is licensed under the MIT-0 License. See the LICENSE file.

