#!/bin/sh
sudo chmod -R 777 data/
docker build -t enovation .
docker run -p 8000:80 enovation
