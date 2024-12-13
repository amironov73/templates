@echo off

docker run -d --name grafana --link prometheus -p 3000:3000 grafana/grafana