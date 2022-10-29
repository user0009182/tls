REM generates a CA certificate, a Server certificate and a client certificate
set path=%path%;D:\old\Program Files\Git\usr\bin
mkdir -p certificates
set curdir=%cd%
pushd certificates

REM == Generate CA certificate ==
REM generate CA private key
openssl genrsa -out cakey.pem 2048
REM generate CA certififcate
openssl req -x509 -new -nodes -key cakey.pem -sha256 -days 1825 -subj "/CN=testCA" -out cacert.pem

REM == Generate Server certificate ==
REM generate server private key
openssl genrsa -out serverkey.pem 2048
REM generate certificate request
openssl req -new -key serverkey.pem -out server.csr -config ..\servercsr.conf
REM issue server certificate
openssl x509 -req -in server.csr -CA cacert.pem -CAkey cakey.pem -CAcreateserial -out servercert.pem -days 10000 -extfile ..\servercsr.conf

REM == Generate Client certificate ==
REM generate client private key
openssl genrsa -out clientkey.pem 2048
REM generate certificate request
openssl req -new -key clientkey.pem -out client.csr -config ..\clientcsr.conf
REM issue client certificate
openssl x509 -req -in client.csr -CA cacert.pem -CAkey cakey.pem -CAcreateserial -out clientcert.pem -days 10000 -extfile ..\clientcsr.conf
popd