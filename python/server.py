#creates a TLS enabled server running on localhost:10101
#clients must provide a valid client certificate
import ssl
import socket
import os
import time
from pathlib import Path

#change current directory so certificate files can be referenced relatively
script_path=Path.joinpath(Path(__file__).parent.parent)
os.chdir(script_path)

context = ssl.SSLContext(ssl.PROTOCOL_TLS_SERVER)
context.load_cert_chain("certificates/servercert.pem", keyfile="certificates/serverkey.pem")
context.load_verify_locations('certificates/cacert.pem')
#require the client provide a valid client certificate
context.verify_mode = ssl.CERT_REQUIRED
with socket.socket(socket.AF_INET, socket.SOCK_STREAM, 0) as sock:
    sock.bind(('127.0.0.1', 10101))
    print("listening on localhost:10101")
    sock.listen(5)
    with context.wrap_socket(sock, server_side=True) as ssock:
        conn, addr = ssock.accept()
        print("connection accepted")

        #obtain the client certificate
        #the client certificate has already been validated against the CA
        client_certificate=conn.getpeercert()
        #validate the hostname in the client certificate. This will raise a CertificateError
        #if the hostnames do not match
        ssl.match_hostname(client_certificate, "client.com")
        print("client connected OK")
        conn.send("some data".encode("ascii"))
        time.sleep(5)