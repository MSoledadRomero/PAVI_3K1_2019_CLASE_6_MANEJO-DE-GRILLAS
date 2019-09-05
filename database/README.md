# BugTracker - Base de datos

**![](https://lh4.googleusercontent.com/FJpub1nDGJkY-n2G9CykG3WqpKdvo2d_w6dsdzBlxcrV7YAc4aH6-cxEaOSuOEFMwGJwbSs8DoOSph3wpSO-ac0NrM8f9Gr2jFLpdkEW5dNMQgtLYeuvjiVJ0QhKpHCEZszj8Kaz)**

Tabla **Bugs**:

 - [PK] id_bug (int)
 - titulo (varchar(100))		 
 - descripcion (varchar(1000))
 - fecha_alta (date)
 - [FK] id_usuario_responsable  (int)
 - [FK] id_usuario_asignado  (int)		 
 - [FK] id_producto  (int)				
 - [FK] id_prioridad  (int)			 
 - [FK] id_criticidad  (int)			 
 - [FK] id_estado  (int)	

Tabla **BugsHistorico**:

- [PK] id_bug_historico (int)
- fecha_hitorico (date)
- titulo (varchar(100))
- descripcion (varchar(1000))
- [FK] id_bug (int)
- [FK] id_usuario_responsable (int)
- [FK] id_usuario_asignado (int)
- [FK] id_producto (int)
- [FK] id_prioridad (int)
- [FK] id_criticidad (int)
- [FK] id_estado (int)

Tabla **Criticidades**:
- [PK] id_criticidad (varchar(50))
- nombre (varchar(50))

Tabla **Estados**:
- [PK] id_estado (int)
- nombre (varchar(50))


Tabla **Formularios**:
- [PK] id_formulario (int)
- nombre (varchar(50))

Tabla **Perfiles**:
- [PK] id_perfil (int)
- nombre (varchar(50))

Tabla **Permisos**:
- [PK] id_formulario (int)
- [PK] id_perfil (int)

Tabla **Prioridades**:
- [PK] id_prioridad (int)
- nombre (varchar(50))

Tabla **Productos**:
- [PK] id_producto (int)
- nombre (varchar(50))

Tabla **Usuarios**:
- [PK] id_usuario (int)
- [FK] id_perfil (int)
- usuario (varchar(50))
- password (varchar(10))
- email (varchar(50))
- estado (varchar(1))


