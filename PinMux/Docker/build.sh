#!/bin/bash -e

# Place this file in your .NET project directory. It doesn't matter where.
# Optionally include the Dockerfile. Without, the default image will be pulled from GitHub.
# Optionally include a copyTo.sh, to copy the executable to a device.

PROJECT_NAME="PinMux" #Project Name (without .csproj extension). If you leave this empty, dotnet will try to build the whole solution (and may error).
SDK="9.0"
ARCH="arm64"
OUTPUT="/bin/$ARCH"


# If Dockerfile is present, then use that.  Otherwise, use image from GitHub
if [ -f "Dockerfile" ]; then
  IMAGE="tinydotnet-AOT-$ARCH-$SDK"
  
  # Check if "build" is in arguments or build-dotnet-arm64 image doesn't exist
  if [[ "$*" == *"build"* ]] || ! docker images | grep -q $IMAGE; then
      echo "Building Docker image..."
      docker build --build-arg SDK=$SDK . -t $IMAGE
  fi  
  
else
  IMAGE="ghcr.io/dottinynet/dockeraot:$ARCH-$SDK"
  docker pull $IMAGE
fi


current_dir=$(pwd)

# Loop to traverse up the directory tree
while [ "$current_dir" != "/" ]; do
    # Check if a .sln file exists in the current directory
    sln_file=$(find "$current_dir" -maxdepth 1 -name "*.sln" | head -n 1)
    if [ -n "$sln_file" ]; then
        # If found, set PROJECTPATH to the directory containing the .sln file
        PROJECTPATH="$current_dir"
        break
    fi
    # Move up one directory level
    current_dir=$(dirname "$current_dir")
done

if [ -n "$PROJECTPATH" ]; then
     echo "Project directory found: $PROJECTPATH"
 else
     echo "No .sln file found in the directory tree"
     exit 1
 fi

set +e

OUTPUTPATH=$PROJECTPATH/$OUTPUT

rm -f $OUTPUTPATH/*
docker run --rm -it --net=host -u $(id -u):$(id -g) -v $PROJECTPATH:/src $IMAGE $PROJECT_NAME

# ----- Optional Copy 

if [ -f "copyTo.sh" ]; then
  source copyTo.sh $@
fi
