# Docker commands
```
docker run -d --name oracle_standard_express -p 1521:1521 -p 5500:5500 -e ORACLE_PWD=Password_123# container-registry.oracle.com/database/express:latest
docker run -d --name oracle_standard_free -p 1521:1521 -p 5500:5500 -e ORACLE_PWD=Password_123# container-registry.oracle.com/database/free:latest
docker run -d --name keycloak -p 8080:8080 -e KC_BOOTSTRAP_ADMIN_USERNAME=admin -e KC_BOOTSTRAP_ADMIN_PASSWORD=admin quay.io/keycloak/keycloak:26.2.5 start-dev
```
# Podman commands
```
podman run -d --name oracle_standard_express -p 1521:1521 -p 5500:5500 -e ORACLE_PWD=Password_123# container-registry.oracle.com/database/express:latest
podman run -d --name oracle_standard_free -p 1521:1521 -p 5500:5500 -e ORACLE_PWD=Password_123# container-registry.oracle.com/database/free:latest
podman run -d --name keycloak -p 8080:8080 -e KC_BOOTSTRAP_ADMIN_USERNAME=admin -e KC_BOOTSTRAP_ADMIN_PASSWORD=admin quay.io/keycloak/keycloak:26.2.5 start-dev
```

# TODOs
https://deepwiki.com/nhibernate/fluent-nhibernate/5.1-persistence-specification

# Ideas
Keycloak 26 + manager package
```
docker run --name importedKeyloack -e KEYCLOAK_ADMIN=admin -e KEYCLOAK_ADMIN_PASSWORD=admin -v $(pwd)/my-config:/opt/keycloak/data/import   quay.io/keycloak/keycloak:latest start --import-realm
```
copilot prompt:
```
c# swagger authorization keycloak
```