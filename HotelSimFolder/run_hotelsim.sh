#!/bin/bash

# Navigate to the script's directory
cd "$(dirname "$0")"

# Ensure the executable exists
if [ ! -f "./FinalBuild.exe" ]; then
    echo "Error: FinalBuild.exe not found."
    exit 1
fi

# Grant execution permissions to the executable
chmod +x ./FinalBuild.exe

# Run the executable
echo "Running HotelSim..."
./FinalBuild.exe

echo "HotelSim execution completed."
