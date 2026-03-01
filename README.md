Sebelum menjalankan project, install dulu:
NET 8.0
✅ 1. Install .NET 8 SDK
      - Setelah install cek apakah berhasil, jalankan di command promp: dotnet --version
      - Harus muncul versi 8.x.x
✅ 2. Install Node.js (LTS)
      - Setelah install cek apakah berhasil, jalankan di command promp: node -v dan npm -v
✅ 3. Menjalankan Backend (.NET 8 API)
      - cd AuthWithJWT_Net8
      - Restore dependency: dotnet restore
      - Jalankan API: dotnet run
      - Jika berhasil, akan muncul: Now listening on: https://localhost:xxxx
      - Akan berjalan di : https://localhost:7055; dan http://localhost:5249
✅ 4. Menjalankan Frontend (Vue 3)
      - cd AuthWithJWT_Vue
      - buka CMD dan jalankan : npm install
      - setelah selesai kemudian: npm run dev
      - Jika berhasil akan muncul: http://localhost:5173
      - Buka di browser: http://localhost:5173
      - Akun yang sudah dibuat: 
        a. email: agung@mail.com password: agung123
        b. email: laksono@mail.com password: laksono123
