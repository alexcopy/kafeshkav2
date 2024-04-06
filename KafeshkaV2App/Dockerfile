# Stage 1: Build Angular application
FROM node:20-alpine as builder

# Install Angular CLI globally
RUN npm install -g @angular/cli

# Set the working directory
WORKDIR /app

# Copy package.json and package-lock.json
COPY package*.json ./

# Install dependencies
RUN npm install

# Copy the rest of the application code
COPY . .

# Build the Angular application (assuming you're using Angular)
RUN npm run build --prod

# Stage 2: Serve Angular application with NGINX
FROM nginx:1.19-alpine

# Remove default NGINX website
RUN rm -rf /usr/share/nginx/html/*

# Copy the built Angular application from the previous stage to NGINX's default public directory
COPY --from=builder /app/dist/kafeshka-v2-app/browser /usr/share/nginx/html
COPY nginx.conf /etc/nginx/conf.d/default.conf
# Expose port 80 to the outside world
EXPOSE 80

# Start NGINX server when the container starts
CMD ["nginx", "-g", "daemon off;"]
