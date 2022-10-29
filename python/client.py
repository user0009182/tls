#established client connection to a TLS enabled server running on localhost:10101
#server certificate is verified and a client certificate is supplied
import ssl
import socket
import os
import time
from pathlib import Path

#change current directory so certificate files can be referenced relatively
script_path=Path.joinpath(Path(__file__).parent.parent)
os.chdir(script_path)

#create_default is recommended, although as a side effect it may verifying using trusted CA certificates
#installed on the machine, whereas for this test only the self-signed cacert CA certificate needs to be used
context = ssl.create_default_context()
#use the CA certificate for verifying certificate received by the server
context.load_verify_locations('certificates/cacert.pem')
#identify self using the client certificate
context.load_cert_chain("certificates/clientcert.pem", keyfile="certificates/clientkey.pem")

#connect TCP socket to the server then wrap it in secure socket
#the hostname in the received server certificate is expected to be test.com
with socket.create_connection(("localhost", 10101)) as sock:
    with context.wrap_socket(sock, server_hostname="test.com") as ssock:
        print("connected")
        print(ssock.version())
        ssock.send("some data".encode("ascii"))
        time.sleep(10)