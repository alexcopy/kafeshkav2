#!/bin/bash
set -e
set -x
# Load variables from .env file
if [ -f .env ]; then
  source .env
else
  echo "Error: .env file not found."
  exit 1
fi

# Export data from MySQL container
# Create a MySQL configuration file with credentials
echo "[client]" > mysql_config.cnf
echo "user=$MYSQL_USERNAME" >> mysql_config.cnf
echo "password=$MYSQL_PASSWORD" >> mysql_config.cnf

# Copy the MySQL configuration file into the container
docker cp  mysql_config.cnf "$CONTAINER_NAME:/etc/mysql_config.cnf"
docker exec -i "$CONTAINER_NAME" mysqldump --defaults-extra-file=/etc/mysql_config.cnf -u "$MYSQL_USERNAME" "$MYSQL_DATABASE" > "$OUTPUT_FILE"

# Export data from MySQL container
docker cp "$OUTPUT_FILE" "$CONTAINER_NAME:/docker-entrypoint-initdb.d/"

# Create a new Docker image based on the running container
docker commit "$CONTAINER_NAME" "$DOCKERHUB_USERNAME/$REPOSITORY_NAME:latest"

# Log in to Docker Hub
docker login

# Push the new image to Docker Hub
docker push "$DOCKERHUB_USERNAME/$REPOSITORY_NAME:latest"

# Clean up: remove the SQL dump file
#rm "$OUTPUT_FILE"
echo "Docker image has been created and pushed to Docker Hub. Backup SQL file deleted."
exit 0