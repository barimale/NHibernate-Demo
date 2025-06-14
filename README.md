# Docker commands
```
docker run -d --name oracle_standard_express -p 1521:1521 -p 5500:5500 -e ORACLE_PWD=Password_123# container-registry.oracle.com/database/express:latest
docker run -d --name oracle_standard_free -p 1521:1521 -p 5500:5500 -e ORACLE_PWD=Password_123# container-registry.oracle.com/database/free:latest
```
# Podman commands
```
podman run -d --name oracle_standard_express -p 1521:1521 -p 5500:5500 -e ORACLE_PWD=Password_123# container-registry.oracle.com/database/express:latest
podman run -d --name oracle_standard_free -p 1521:1521 -p 5500:5500 -e ORACLE_PWD=Password_123# container-registry.oracle.com/database/free:latest
```

# TODOs
https://deepwiki.com/nhibernate/fluent-nhibernate/5.1-persistence-specification

# Ideas
Keycloak 26 + manager package
```
https://www.keycloak.org/getting-started/getting-started-docker
https://medium.com/@faulycoelho/net-web-api-with-keycloak-11e0286240b9
```