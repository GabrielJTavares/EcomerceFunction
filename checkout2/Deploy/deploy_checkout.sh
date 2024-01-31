#!/usr/bin/env bash
# exit when any command fails
set -e

APPNAME="ecomercegjt"

cd ../
func azure functionapp publish $APPNAME