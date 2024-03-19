# Use an official Node.js runtime as a base image
FROM node:21-alpine as builder

# Set the working directory inside the container
WORKDIR /app

# Copy package.json and package-lock.json files
COPY package*.json ./

# Install dependencies
RUN npm install

# Copy the rest of the application code
COPY . .

# Build the Angular app for production
RUN npm run build -- --prod

# Use a lightweight base image for the final stage
FROM nginx:alpine

# Copy the built Angular app from the previous stage into the NGINX server's public directory
COPY --from=builder /app/dist/angular-app /usr/share/nginx/html

# Expose port 80
EXPOSE 80
