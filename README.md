# tls
Test of TLS, generating certificates, setting up secure server/client, etc

The openssl tool is used and has to be on the path. On Windows the openssl.exe that comes with Git can be used.

These scripts can be run in sequence:

create_certs.bat        generates keys & CA, Server & Client certificates. These are output to a certificates subfolder
start_server.bat        starts a debug TLS secured server using the generated certificates
start_client.bat        connects a debug TLS client to the started server

# create_certs.bat

Drives various openssl commands to produce a CA certificate and a server certificate signed by that CA. The clientcsr.conf and servercsr.conf files used as input in the process set up the details of the server and client certificates.

outputs
- cacert.pem        CA certificate
- cakey.pem         CA private key
- servercert.pem    server certificate
- serverkey.pem     server private key
- clientcert.pem    client certificate
- clientkey.pem     client private key

Steps modified from [here](https://superhero.ninja/2015/07/22/create-a-simple-https-server-with-openssl-s_server/)



