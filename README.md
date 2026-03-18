# 🛡️ Military Inventory System
> .NET tabanlı, yüksek güvenlikli ve detaylı envanter takip çözümü.

[![C#](https://img.shields.io/badge/Language-C%23-blue.svg)](https://docs.microsoft.com/en-us/dotnet/csharp/)
[![.NET 8](https://img.shields.io/badge/Framework-.NET%208-purple.svg)](https://dotnet.microsoft.com/download)
[![SQL Server](https://img.shields.io/badge/Database-SQL%20Server-red.svg)](https://www.microsoft.com/en-us/sql-server/)

---

## 🚀 Proje Hakkında
Bu sistem, askeri mühimmat, araç ve teçhizatın dijital ortamda **gerçek zamanlı** olarak takip edilmesini, personele zimmetlenmesini ve tüm geçmiş hareketlerin (AuditLog) raporlanmasını sağlar.

### ✨ Öne Çıkan Özellikler
*   **📋 Envanter Yönetimi:** Detaylı ekipman kayıtları ve kategorizasyon.
*   **👤 Personel Atama:** Ekipmanların personel üzerine atanması ve zimmet geçmişi.
*   **🛡️ AuditLog:** Sistemdeki her değişikliğin (Kim, Ne zaman, Hangi veriyi değiştirdi?) kaydı.
*   **📊 RESTful Mimari:** Tüm platformlarla entegre edilebilir hızlı API yapısı.

---

## 🛠️ Kullanılan Teknolojiler

| Alan | Teknoloji |
| :--- | :--- |
| **Backend** | .NET Core Web API (C#) |
| **ORM** | Entity Framework Core |
| **Veritabanı** | Microsoft SQL Server (SSMS) |
| **Loglama** | Custom Audit Logging System |

---

## 📂 Klasör Yapısı
```text
MilitaryInventory/
├── Controllers/      # API Endpoint'leri (Personnel, Equipment, AuditLog...)
├── Models/           # Veritabanı tabloları (Entities)
├── Data/             # DbContext ve Veritabanı konfigürasyonları
└── Program.cs        # Uygulama başlangıç ayarları
