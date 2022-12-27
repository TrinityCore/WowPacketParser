#!/bin/sh

cd $(dirname "$1")
/app/WowPacketParser "$1"
