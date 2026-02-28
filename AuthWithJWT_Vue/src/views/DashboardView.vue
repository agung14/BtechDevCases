<template>
  <v-card class="pa-6" rounded="xl" elevation="6">
    <h2 class="text-h5">
      Hello {{ email }}, welcome back 👋
    </h2>
  </v-card>
</template>

<script setup lang="ts">
import { ref, onMounted } from 'vue'
import { getToken, parseJwt } from '@/services/authService'

const email = ref('')

onMounted(() => {
  const token = getToken()
  if (token) {
    const payload = parseJwt(token)
    email.value = payload?.email || ''
  }
})
</script>