# 🚀 Cara Menjalankan Project

## 📌 Requirement

Sebelum menjalankan project, install terlebih dahulu:

### ✅ 1. Install .NET 8 SDK

Setelah install, cek apakah berhasil dengan menjalankan di Command Prompt:

```bash
dotnet --version
```

Harus muncul versi:

```
8.x.x
```

---

### ✅ 2. Install Node.js (LTS)

Setelah install, cek apakah berhasil dengan menjalankan:

```bash
node -v
npm -v
```

---

## 🔧 3. Menjalankan Backend (.NET 8 API)

Masuk ke folder backend:

```bash
cd AuthWithJWT_Net8
```

Restore dependency:

```bash
dotnet restore
```

Jalankan API:

```bash
dotnet run
```

Jika berhasil, akan muncul:

```
Now listening on: https://localhost:xxxx
```

Backend akan berjalan di:

* [https://localhost:7055](https://localhost:7055)
* [http://localhost:5249](http://localhost:5249)

---

## 🎨 4. Menjalankan Frontend (Vue 3)

Masuk ke folder frontend:

```bash
cd AuthWithJWT_Vue
```

Install dependency:

```bash
npm install
```

Setelah selesai, jalankan:

```bash
npm run dev
```

Jika berhasil, akan muncul:

```
http://localhost:5173
```

Buka di browser:

```
http://localhost:5173
```

---

## 🔐 Akun Login yang Sudah Dibuat

### a.

* Email: `agung@mail.com`
* Password: `agung123`

### b.

* Email: `laksono@mail.com`
* Password: `laksono123`
