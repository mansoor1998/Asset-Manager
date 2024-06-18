#!/bin/bash

# Define an array with the file paths
files=(
    "commands-depl.yml" 
    "platforms-depl.yml" 
    "platforms-np-srv.yml" 
)

    # "ingress-srv.yml"
    # "local-pvc.yml" 
    # "mssql-plat-depl.yml" 

# Loop through the array and apply each file
for file in "${files[@]}"; do
    echo "Applying configuration from $file"
    kubectl apply -f "$file"
done
