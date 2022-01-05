# Challenge
Desafio para entrevista

- Ir al directorio \Challenge\Challenge
- Ejecutar el comando **docker build -t microservicio:v1 .**
- Ejecutar el comando **docker run -p 5000:5000 microservicio:v1**
- Descargar la coleccion de postman https://www.getpostman.com/collections/d3e36895dbdb016614d2
- La cadena de conexion para MongoDB es **mongodb+srv://slazarte:48006321Benja@miclustercafe.n5ymv.mongodb.net**

### Nota
- Ya se encuentran generados las coleciones bancos, medios de pagos, categorias y promociones. Pense que crear colecciones para bancos, medios de pago y categorias seria mejor que ponerlas en duro en el codigo. Estoy usando un Servidor MongoDB remoto.
- Los Ejercicios se encuentran resueltos en el proyecto ejercicios. Para el ejercicio 4 planteo 2 posibles soluciones: Usando interfaz y clase abstracta.
- Hice algunos cambios en la entidad Promocion ya que como estaba propuesta en el documento no funciona al momento de recibir el body del post. Tambien uso el ObjectId de MongoDb en vez del GUID
- Con respecto a las pruebas realice algunas de ejemplo y se encuentran en el proyecto Callenge.Test

La verdad es que recibi este desafio el 31 de diciembre con las fiestas encima y con familia de visitas en casa. 
Sumado que empece a trabajar el lunes, asi que hice lo mejor que pude en el poco tiempo que tuve para hacerlo. Sepan disculpar. 
