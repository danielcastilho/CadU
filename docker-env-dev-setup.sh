#!/bin/bash
docker pull postgres
docker pull dpage/pgadmin4
docker network create --driver bridge cadu
docker run --name cadu-postgres --network=cadu -e "POSTGRES_PASSWORD=CadU2020!" -p 5432:5432 -v ~/Desenvolvimento/PostgreSQL:/var/lib/postgresql/data -d postgres
docker run --name cadu-pgadmin --network=cadu -p 15432:80 -e "PGADMIN_DEFAULT_EMAIL=mail@gmail.com" -e "PGADMIN_DEFAULT_PASSWORD=CadU2020!" -d dpage/pgadmin4
docker ps