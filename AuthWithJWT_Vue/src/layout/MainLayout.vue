<template>
  <v-app>
    <v-app-bar color="primary" dark>
      <v-toolbar-title>My Banking App</v-toolbar-title>

      <v-spacer />

      <v-btn variant="text" @click="goDashboard">
        Dashboard
      </v-btn>

      <v-btn variant="text" @click="goTransfer">
        Transfer Funds
      </v-btn>

      <v-btn variant="text" @click="handleLogout">
        Logout
      </v-btn>
    </v-app-bar>

    <v-main>
      <v-container class="mt-6">
        <router-view />
      </v-container>
    </v-main>
  </v-app>
</template>

<script setup lang="ts">
import { useRouter } from 'vue-router'
import { logout } from '@/services/authService'

const router = useRouter()

const goDashboard = () => router.push('/dashboard')
const goTransfer = () => router.push('/transfer')

const handleLogout = () => {
  logout()
  router.push('/login')
}
</script>