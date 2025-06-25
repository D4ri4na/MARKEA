
🛍️ MARKEA - Plataforma de Comercio Electrónico
MARKEA es un mercado digital C2C (consumidor a consumidor) moderno, intuitivo y eficiente, diseñado para facilitar la conexión entre vendedores y compradores en un entorno seguro y fácil de usar.

Tabla de Contenidos
Introducción

Características Principales

Stack Tecnológico

Estructura del Repositorio

Puesta en Marcha (Guía de Instalación)

Autores

Introducción
En el panorama digital actual, las plataformas de comercio electrónico se han convertido en un pilar fundamental de la economía, facilitando la conexión entre vendedores y compradores. El proyecto MARKEA nace de la necesidad de crear un espacio digital C2C (consumidor a consumidor) que sea moderno, intuitivo y eficiente. Esta plataforma permite a los usuarios no solo adquirir productos, sino también convertirse en vendedores, gestionando sus propias publicaciones de una manera sencilla.

Para lograr la robustez y escalabilidad requeridas, el proyecto se ha desarrollado utilizando un stack tecnológico moderno: React para una interfaz de usuario dinámica y reactiva; .NET para construir una API RESTful segura y potente; SQL Server para la gestión de datos transaccionales y estructurados; y MongoDB para el almacenamiento flexible de datos no estructurados, como las imágenes de productos. Esta arquitectura híbrida de bases de datos es una decisión de diseño clave, permitiendo que cada sistema gestione el tipo de datos para el que es más eficiente.

Este documento detalla el ciclo de vida completo del proyecto, desde su concepción y análisis de requisitos hasta el diseño, las pruebas y la conclusión, sirviendo como una guía integral de su arquitectura y funcionalidades.

Características Principales
Plataforma C2C: Permite que cualquier usuario registrado pueda comprar y vender productos.

Gestión de Publicaciones: Los usuarios pueden crear, ver, editar y eliminar sus propios productos en venta.

Perfil de Usuario: Un panel centralizado donde los usuarios pueden gestionar su información personal, ver sus productos a la venta y su historial de compras.

Autenticación y Seguridad: Sistema de registro e inicio de sesión para proteger las cuentas de usuario.

Interfaz Moderna: Experiencia de usuario fluida y reactiva construida con React.

Stack Tecnológico
La arquitectura del proyecto se divide en tres componentes principales:

Frontend
Framework: React

Descripción: Se utiliza para construir la interfaz de usuario, garantizando una experiencia dinámica, interactiva y de una sola página (SPA).

Backend (API)
Framework: .NET

Descripción: Construye una API RESTful robusta y segura que maneja toda la lógica de negocio, la autenticación de usuarios y la comunicación con las bases de datos.

Bases de Datos
Sistema de Gestión de Base de Datos Relacional (RDBMS): Microsoft SQL Server

Uso: Almacena datos estructurados y transaccionales como información de usuarios, detalles de productos (nombre, precio, descripción), y registros de ventas.

Sistema de Gestión de Base de Datos NoSQL: MongoDB

Uso: Diseñado para almacenar datos no estructurados de forma flexible. En MARKEA, se encarga de gestionar las referencias a las imágenes de los productos, permitiendo escalabilidad y eficiencia.

Estructura del Repositorio
El proyecto está organizado en los siguientes directorios principales:

MARKEA/
├── 📁 MarkeaApi/      # Código fuente de la API en .NET.
├── 📁 interfaz/       # Código fuente del frontend en React.
├── 📁 BDD/            # Scripts, diagramas y backups de la base de datos SQL Server.
├── 📄 .gitignore      # Archivos y carpetas ignorados por Git.
└── 📄 README.md       # Este archivo.
Puesta en Marcha
Para ejecutar este proyecto de forma local, sigue estos pasos:

Prerrequisitos
Node.js (versión 18 o superior)

SDK de .NET (versión 7 o superior)

Microsoft SQL Server

MongoDB

1. Configuración de la Base de Datos
Abre SQL Server Management Studio.

Restaura la base de datos utilizando el archivo .bak que se encuentra en la carpeta /BDD.

Asegúrate de que tu servicio de MongoDB esté en ejecución.

2. Configuración del Backend (.NET API)
Bash

# Navega a la carpeta de la API
cd MarkeaApi

# Actualiza la cadena de conexión a la base de datos en appsettings.json
# con tus credenciales de SQL Server y MongoDB.

# Instala las dependencias
dotnet restore

# Inicia el servidor de la API
dotnet run
La API estará disponible en https://localhost:7XXX (o el puerto configurado).

3. Configuración del Frontend (React)
Bash

# Abre una nueva terminal y navega a la carpeta del frontend
cd interfaz

# Instala las dependencias de Node.js
npm install

# Inicia la aplicación de React
npm start
La aplicación se abrirá automáticamente en tu navegador en http://localhost:3000.

Autores
[Tu Nombre Completo] - Desarrollo Completo - Tu-Usuario-de-GitHub
