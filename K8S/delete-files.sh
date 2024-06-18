#!/bin/bash

# Define an array with the file paths
files=(
    "commands-depl.yml" 
    "platforms-depl.yml" 
    "platforms-np-srv.yml" 
    "mssql-plat-depl.yml" 
    "local-pvc.yml" 
    "ingress-srv.yml"
)

# Loop through the array and apply each file
for file in "${files[@]}"; do
    echo "Applying configuration from $file"
    kubectl delete -f "$file"
done
