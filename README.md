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