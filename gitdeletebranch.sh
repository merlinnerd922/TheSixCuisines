#!/bin/sh

git push origin --delete "$1";
git branch -d "$1";