# Online Randevu Yönetim Sistemi

Bu proje, kullanıcıların çeşitli hizmetler için randevu alabileceği ve yöneticilerin bu randevuları yönetebileceği bir web uygulamasıdır.

## Proje Özellikleri

### Kullanıcı Özellikleri
- Randevu oluşturma
- Randevu güncelleme
- Randevu iptal etme
- Randevu listesini görüntüleme

### Admin Özellikleri
- Tüm randevuları görüntüleme
- Randevu durumlarını güncelleme (Beklemede, Onaylandı, İptal Edildi, Tamamlandı)
- Kullanıcı yönetimi
- Rol bazlı yetkilendirme

## Teknoloji Stack

### Backend
- .NET 8
- ASP.NET Core MVC
- Entity Framework Core
- MSSQL
- Clean Architecture
- CQRS Pattern with MediatR
- Cookie Based Authentication
- Role Based Authorization
- BCrypt for Password Hashing

### Frontend
- jQuery
- Bootstrap 5
- AJAX
- Toastr Notifications
- JavaScript ES6+

## Mimari

Proje Clean Architecture prensiplerine uygun olarak 4 katmanlı bir yapıda geliştirilmiştir:

```
AppointmentManagementSystem/
├── Core/
│   ├── Domain/           # Entities, Enums, Exceptions
│   └── Application/      # Interfaces, DTOs, CQRS Handlers
├── Infrastructure/       # DbContext, Migrations, External Services
└── WebUI/               # Controllers, Views, Frontend Assets
```

### Domain Katmanı
- Temel entity'ler (User, Role, Appointment, Service)
- Enum'lar (AppointmentStatus)
- Domain Events
- Base Entities

### Application Katmanı
- CQRS Commands/Queries
- DTOs
- Interfaces
- Validations
- Business Rules

### Infrastructure Katmanı
- Entity Framework Configurations
- Repository Implementations
- Database Migrations
- Identity Services
- External Service Implementations

### WebUI Katmanı
- MVC Controllers
- Razor Views
- JavaScript Files
- CSS/Bootstrap Styling
- AJAX Operations

## Kurulum

1. Repository'yi klonlayın 
2. dbdosya.sql dosyasını import edip `appsettings.json` içinde connection string'i ayarlayın 
## Migration   
1. DesignTimeCurrentUserService sınıfındaki connectionstring kısmını Db bağlantısına göre değiştirebilirsiniz. 
2. Package Manager Console'da:
```bash
Update-Database
```
3. Projeyi çalıştırın

## Default Kullanıcılar

```
Admin:
Username: admin
Password: Admin123!

User:
Username: testuser
Password: 123456
```
## Geliştirme Notları

- Code-First yaklaşımı kullanıldı
- Tüm CRUD operasyonları AJAX ile yapıldı
- Cookie tabanlı authentication implementasyonu
- Toastr ile kullanıcı bildirimleri
- Frontend'de rol bazlı UI adaptasyonu

 
