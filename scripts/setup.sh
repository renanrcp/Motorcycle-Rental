#!/bin/bash

docker volume create motorcycle_rental_postgres_data
dotnet tool install --global dotnet-ef