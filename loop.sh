#!/bin/bash

count=0

if ! dotnet test $@; then
  echo "++ Failed on attempt $count";
  exit 0;
fi

count=$(($count + 1));

while dotnet test $@ --no-build; do
  echo "++ Loop $count succeeded";
  echo
  count=$(($count + 1));
done

echo "++ Failed on attempt $count";
exit $count
