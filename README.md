# BlogSite.API

Katmanlı mimari kullanılarak geliştirilmiş bir blog uygulaması.

Core Katmanı:
- Proje içinde katmanlarda kullanılacak service' lerin implementasyonu için generic abstract yapılar oluşturuldu.
- Diğer projelere direkt enjekte edilebilmesi için tüm service' ler generic oluşturuldu.
- Bu katmanda, DataAccess Katmanında kullanılacak repository service' leri generic halde oluşturuldu. Entity Katmanında oluşturulan ViewModel' ler için Validation Tool yazıldı ve hata yönetimi yapıldı. Service' lerin kullanımı sırasında kullanıcak generic response yapısı yaratıldı.

Entities Katmanı:
- Üç adet temel entity kullanıldı.
- CRUD işlemleri için ViewModel' lar oluşturuldu.

DataAccess Katmanı:
- Core Katmanında oluşturulan generic service yapılarının concrete halleri her entity' e uygun oluşturuldu.
- Veritabanının hızlı oluşturulması için EF Core migration yapısının kullanılmasına karar verildi. DbContext yapısı oluşturuldu.
- Her entity için oluşturulan Repository' ler içinde veritabanına erişim ve CRUD işlemleri için ADO.NET kullanıldı.
- Ayrıca sistemin loose coupled özelliğini göstermek amacıyla Repository yapısı aynı zamanda Dapper kullanılarak da gösterildi.

Business Katmanı:
- Core Katmanında oluşturulan generic service yapılarının concrete halleri her entity' e uygun oluşturuldu.
- Projenin feature' larına göre business kodları yazıldı.
- Post ve Comment entity' lerinin Create, Update ve Delete işlemlerini üstlenecek olan Cache Microservice' i için concrete yapılar oluşturuldu.
- Cache' deki veriyi direkt cache' den okumak için Cache Service' i oluşturuldu.
- ViewModel' lerin dönüştürülmesi için AutoMapper kütüphanesi kullanıldı ve Core Katmanında oluşturulan ValidationTool ile isteklerin validation' ları oluşturuldu.
- Gelen request' lere dönecek response mesajları için constant sınıfı oluşturuldu ve kod text' lerden arındırıldı.
- Authentication işlemleri için TokenHandler sınıfı oluşturuldu. Login, Register ve token işlemleri için AuthService oluşturuldu.
- Hataların takip edilmesi için ElasticSearch ve Kibana ile Log' lama mekanizması oluşturuldu.

API:
- Klasik Web API kullanıldı.
- Business Katmanındaki service' ler dependency injection ile API' ler içerisinde kullanıldı.
- Post ve Comment entity' lerinin Create, Update ve Delete işlemlerini üstlenecek olan Cache Microservice' i ile iletişimi sağlayacak olan RabbitMQ Masstransit yapısı oluşturuldu. Queue yapısı ile  Create, Update ve Delete işlemleri Cache Microservice üzerinden yapıldı.

Caching Microservice:
- RabbitMQ sistemi ile WebAPI' den gelecek Create, Update ve Delete isteklerini MassTransit ile Consumer' lara dağıtıldı.
- Generic olarak yapılmış cache service' ler ve Dapper kullanılarak yapılmış generic repository' ler kullanıldı.
- Gelen request' ler MassTransit consumers içerisinde yapılan service' ler ile halledildi.
