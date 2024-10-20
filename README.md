# RentaCarApp
[![Türkçe](https://img.shields.io/badge/dil-Türkçe-red)](#Türkçe)
[![English](https://img.shields.io/badge/language-English-blue)](#English)
[![Resimler](https://img.shields.io/badge/resimler-Göster-orange)](#Resimler)

---
<a name="Türkçe"></a>
## 🇹🇷 Türkçe

Bu proje, **TechCareer.net** tarafından düzenlenen **".NET Core Developer Dive Bootcamp"** bitirme projesi olarak geliştirilmiştir. Proje, bootcamp süresince edinilen beceri ve bilgileri kullanarak .NET Core, MVC, Razor Pages, Entity Framework ve web geliştirme alanlarındaki uzmanlığı ortaya koymaktadır.

## Kullanılan Teknolojiler

- **.NET Core**: Uygulamanın ana çerçevesi.
- **Razor Pages**: Kullanıcı arayüzünü oluşturmak için.
- **SQLite**: Uygulama verilerini saklamak için kullanılan veritabanı.
- **Entity Framework Core**: Veritabanı işlemlerini yönetmek için (ORM).

## Özellikler

- Mevcut araçları listeleyebilme.
- Marka ve modele göre araç arama.
- Detaylı araç bilgilerini görüntüleyebilme.
- Araçları yönetmek için yönetici paneli (CRUD işlemleri).
- Araçları marka ve duruma (aktif/pasif) göre filtreleme.

## Gereksinimler

Başlamadan önce aşağıdaki yazılımların yüklü olduğundan emin olun:

- [.NET 8 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/8.0)
- [SQLite](https://www.sqlite.org/download.html) (Çoğu sistemde ek kurulum gerektirmez, yerleşik olarak çalışır.)
- [Visual Studio](https://visualstudio.microsoft.com/) veya [Visual Studio Code](https://code.visualstudio.com/) gibi bir kod düzenleyici.

## Başlangıç

### 1. Depoyu Klonlayın

```bash
git clone https://github.com/sagdicberk/RentACarApp.git
cd RentACarApp
```

### 2. Bağımlılıkları Yükleyin
Gerekli bağımlılıkları yüklemek için:
```bash
dotnet restore
```

### 3. Uygulamayı Çalıştırın
Uygulamayı çalıştırmak için aşağıdaki komutu kullanın:
```bash
dotnet run
```
Alternatif olarak, Visual Studio veya Visual Studio Code ile "Çalıştır" butonunu kullanarak uygulamayı başlatabilirsiniz.

## Kullanım
* Uygulama çalıştığında, araçları arayabilir, detaylarını görüntüleyebilir ve yönetici paneli aracılığıyla araç envanterini yönetebilirsiniz.
* Yönetici paneli, sistemdeki araçları ekleme, düzenleme, silme ve görüntüleme işlemlerini yapmanıza olanak tanır.
---
<a name="English"></a>
## 🇬🇧 English
---

This project was created as the final assignment for the **".NET Core Developer Dive Bootcamp"** organized by **TechCareer.net**. It reflects the skills and knowledge gained throughout the bootcamp, focusing on .NET Core, MVC, Razor Pages, Entity Framework, and web development practices.

## Technologies Used

- **.NET Core**: The primary framework for building the application.
- **Razor Pages**: For building the user interface.
- **SQLite**: As the database for storing application data.
- **Entity Framework Core**: For handling database interactions (ORM).

## Features

- View available cars
- Search cars by brand and model
- View detailed car information
- Admin panel for managing cars (CRUD operations)
- Filter cars by brand and status (active/inactive)

## Prerequisites

Before you begin, make sure you have the following installed:

- [.NET 8 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/8.0)
- [SQLite](https://www.sqlite.org/download.html) (No additional setup is needed for most systems, as it runs in-process)
- A code editor like [Visual Studio](https://visualstudio.microsoft.com/) or [Visual Studio Code](https://code.visualstudio.com/)

## Getting Started

### 1. Clone the repository

```bash
git clone https://github.com/sagdicberk/RentACarApp.git
cd RentACarApp
```

### 2. Restore dependencies
Make sure all the required dependencies are installed:
```bash
dotnet restore
```

### 3. Run the application
Start the application using the following command:
```bash
dotnet run
```
Alternatively, you can use Visual Studio or Visual Studio Code to launch the application using the "Run" button.

## Usage
* Once the application is running, you can search for cars, view their details, and use the admin panel to manage the car inventory.
* The admin panel allows you to add, edit, delete, and view cars in the system.


<a name="Resimler"></a>
## 📸 Resimler

Aşağıda uygulamanın ekran görüntüleri bulunmaktadır:

![Ana Sayfa](SS/Home.png)
![Araç Listesi](SS/Index-Car.png)
![Araç Detayı](SS/Detail.png)
![Admin](SS/List-Car.png)
![Araç Güncelleme](SS/Update-Car.png)
![Admin](SS/Index-Brand.png)