@echo off

docker run -d -e ACCEPT_EULA=y -p 5341:80 datalust/seq
