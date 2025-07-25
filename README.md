# Docker commands
```
docker run -d --name oracle_standard_express -p 1521:1521 -p 5500:5500 -e ORACLE_PWD=Password_123# container-registry.oracle.com/database/express:latest
docker run -d --name oracle_standard_free -p 1521:1521 -p 5500:5500 -e ORACLE_PWD=Password_123# container-registry.oracle.com/database/free:latest
docker run --name importedKeyloack -p 8080:8080 -e KEYCLOAK_ADMIN=admin -e KEYCLOAK_ADMIN_PASSWORD=admin -v ./keycloak/imports:/opt/keycloak/data/import quay.io/keycloak/keycloak:latest start-dev --import-realm
```
# Podman commands
```
podman run -d --name oracle_standard_express -p 1521:1521 -p 5500:5500 -e ORACLE_PWD=Password_123# container-registry.oracle.com/database/express:latest
podman run -d --name oracle_standard_free -p 1521:1521 -p 5500:5500 -e ORACLE_PWD=Password_123# container-registry.oracle.com/database/free:latest
podman run --name importedKeyloack -p 8080:8080 -e KEYCLOAK_ADMIN=admin -e KEYCLOAK_ADMIN_PASSWORD=admin -v ./keycloak/imports:/opt/keycloak/data/import quay.io/keycloak/keycloak:latest start-dev --import-realm
```
