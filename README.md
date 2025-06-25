# 🛍️ MARKEA - Plataforma de Comercio Electrónico C2C

**MARKEA** es un mercado digital **C2C (consumidor a consumidor)** moderno, intuitivo y eficiente, diseñado para facilitar la conexión entre vendedores y compradores en un entorno seguro y fácil de usar.

---

## 📚 Tabla de Contenidos

- [📌 Introducción](#📌-introducción)
- [✨ Características Principales](#✨-características-principales)
- [🛠️ Stack Tecnológico](#🛠️-stack-tecnológico)
- [📁 Estructura del Repositorio](#📁-estructura-del-repositorio)
- [🚀 Puesta en Marcha (Guía de Instalación)](#🚀-puesta-en-marcha-guía-de-instalación)
- [👥 Autores](#👥-autores)

---

## 📌 Introducción

En el panorama digital actual, las plataformas de comercio electrónico son fundamentales en la economía moderna. **MARKEA** nace como una solución **C2C** que permite a los usuarios **comprar y vender productos de forma sencilla**, segura y eficiente.

El proyecto está respaldado por una arquitectura moderna y robusta:

- **Frontend:** React  
- **Backend:** .NET API RESTful  
- **Base de Datos:** SQL Server (estructurados) y MongoDB (no estructurados)

Este README sirve como **guía integral** para comprender el proyecto y ejecutarlo localmente.

---

## ✨ Características Principales

- 🔄 **Plataforma C2C:** Usuarios pueden ser compradores y vendedores.
- 🛒 **Gestión de Publicaciones:** Crear, ver, editar y eliminar productos.
- 👤 **Perfil de Usuario:** Administración de datos personales, productos y compras.
- 🔐 **Autenticación Segura:** Registro, inicio de sesión y manejo de sesiones.
- 💻 **Interfaz Moderna:** Aplicación SPA reactiva y dinámica con React.

---

## 🛠️ Stack Tecnológico

### 🎨 Frontend
- **Framework:** React  
- **Descripción:** Construcción de la SPA con una UI fluida e interactiva.

### ⚙️ Backend
- **Framework:** .NET  
- **Descripción:** API RESTful con toda la lógica de negocio y autenticación.

### 🗄️ Bases de Datos
- **SQL Server:**  
  - Manejo de datos estructurados: usuarios, productos, ventas.  
- **MongoDB:**  
  - Almacenamiento flexible para datos no estructurados: imágenes de productos.

---

## 📁 Estructura del Repositorio
├── 📁 MarkeaApi/ # Código fuente del backend (.NET)
├── 📁 interfaz/ # Código fuente del frontend (React)
├── 📁 BDD/ # Scripts, diagramas y backups de SQL Server
├── 📄 .gitignore # Archivos ignorados por Git
└── 📄 README.md # Este archivo

---

## 🚀 Puesta en Marcha (Guía de Instalación)

### ✅ Prerrequisitos

- Node.js (v18+)
- .NET SDK (v7+)
- Microsoft SQL Server
- MongoDB

---

### 🗃️ 1. Configuración de la Base de Datos

1. Abre **SQL Server Management Studio**.
2. Restaura la base de datos usando el archivo `.bak` en `/BDD`.
3. Asegúrate de que **MongoDB** esté ejecutándose en tu máquina.

---

### 🧩 2. Configuración del Backend (.NET API)

# Navega al backend
cd MarkeaApi

# Actualiza la cadena de conexión en appsettings.json

# Restaura las dependencias
dotnet restore

# Inicia la API
dotnet run
La API estará disponible en https://localhost:7XXX (según tu configuración).

###💻 3. Configuración del Frontend (React)
bash
Copiar
Editar
# Navega al frontend
cd interfaz

# Instala las dependencias
npm install

# Ejecuta la app
npm start
La aplicación se abrirá en http://localhost:3000.

###👥 Autores
Dariana Pol Aramayo – Desarrollo Completo
GitHub: D4ri4na

