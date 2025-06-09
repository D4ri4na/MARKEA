
-- Base de datos
USE MARKEA;

-- Tabla de Usuarios
CREATE TABLE Usuarios (
    id_usuario INT PRIMARY KEY IDENTITY(1,1),
    usuario VARCHAR(100) NOT NULL,
    correo VARCHAR(100) NOT NULL UNIQUE,
    contrasena VARCHAR(255) NOT NULL,
    es_vendedor BIT NOT NULL DEFAULT 0
);

CREATE INDEX idx_usuarios_nombre ON Usuarios(usuario);
CREATE INDEX idx_usuarios_es_vendedor ON Usuarios(es_vendedor);

-- Tabla de Categorías
CREATE TABLE Categorias (
    id_categoria INT PRIMARY KEY IDENTITY(1,1),
    nombre VARCHAR(100) NOT NULL UNIQUE
);

-- Tabla de Productos
CREATE TABLE Productos (
    id_producto INT PRIMARY KEY IDENTITY(1,1),
    id_vendedor INT NOT NULL,
    id_categoria INT,
    nombre VARCHAR(100) NOT NULL,
    descripcion TEXT,
    precio DECIMAL(10,2) NOT NULL CHECK (precio >= 0),
    stock INT NOT NULL DEFAULT 1,
    FOREIGN KEY (id_vendedor) REFERENCES Usuarios(id_usuario),
    FOREIGN KEY (id_categoria) REFERENCES Categorias(id_categoria)
);

CREATE INDEX idx_productos_nombre ON Productos(nombre);
CREATE INDEX idx_productos_id_vendedor ON Productos(id_vendedor);
CREATE INDEX idx_productos_id_categoria ON Productos(id_categoria);

-- Tabla de Ventas
CREATE TABLE Ventas (
    id_venta INT PRIMARY KEY IDENTITY(1,1),
    id_comprador INT NOT NULL,
    id_vendedor INT NOT NULL,
    fecha DATETIME2 DEFAULT GETDATE(),
    estado VARCHAR(20) DEFAULT 'pendiente' CHECK (estado IN ('pendiente', 'pagado', 'enviado', 'entregado', 'cancelado')),
    FOREIGN KEY (id_comprador) REFERENCES Usuarios(id_usuario),
    FOREIGN KEY (id_vendedor) REFERENCES Usuarios(id_usuario)
);

CREATE INDEX idx_ventas_id_comprador ON Ventas(id_comprador);

-- Tabla Detalle de Venta
CREATE TABLE DetalleVenta (
    id_detalle INT PRIMARY KEY IDENTITY(1,1),
    id_venta INT NOT NULL,
    id_producto INT NOT NULL,
    cantidad INT NOT NULL CHECK (cantidad > 0),
    precio_unitario DECIMAL(10,2) NOT NULL CHECK (precio_unitario >= 0),
    FOREIGN KEY (id_venta) REFERENCES Ventas(id_venta) ON DELETE CASCADE,
    FOREIGN KEY (id_producto) REFERENCES Productos(id_producto)
);

CREATE INDEX idx_detalleventa_id_venta ON DetalleVenta(id_venta);
CREATE INDEX idx_detalleventa_id_producto ON DetalleVenta(id_producto);
