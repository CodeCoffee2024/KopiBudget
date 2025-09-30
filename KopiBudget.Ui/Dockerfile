FROM node:20-alpine AS build
WORKDIR /app

# Copy package files
COPY package*.json ./
RUN npm install

# Copy source code
COPY . .

# Build with production optimization
RUN npm run build -- --configuration=production

FROM nginx:alpine

# Remove default nginx config
RUN rm /etc/nginx/conf.d/default.conf

# Copy custom nginx config to the correct location
COPY default.conf /etc/nginx/conf.d/default.conf

# Copy built app
COPY --from=build /app/dist/kopibudget_ui /usr/share/nginx/html

EXPOSE 80
CMD ["nginx", "-g", "daemon off;"]
