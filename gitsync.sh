#!/bin/sh

git add *;
git add .gitignore;
git commit -m "$1";
git push;
git pull;

# Check for the second flag to see if the value is "pushToMaster". If so, then push to that branch as well.
if [[ $# -ge 2 && $2 = "--pushToMaster" ]]; then
   git push origin development:master;
fi
