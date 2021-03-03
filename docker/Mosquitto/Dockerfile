FROM eclipse-mosquitto:2.0.0

WORKDIR /mosquitto

COPY ./mosquitto.conf ./config/
COPY ./mosq-ca.crt .
COPY ./mosq-serv.crt .
COPY ./mosq-serv.key .
COPY ./passwd .

ENTRYPOINT ["/docker-entrypoint.sh"]
CMD ["/usr/sbin/mosquitto", "-c", "/mosquitto/config/mosquitto.conf", "-v"]