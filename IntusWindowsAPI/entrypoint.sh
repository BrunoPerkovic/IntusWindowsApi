#!/bin/bash

set -e

until dotnet ef database update; do
>&2 echo "DB is starting up"
sleep 1
done

>&2 echo "DB is up - executing command"
exec dotnet run --no-launch-profile