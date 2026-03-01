<script setup lang="ts">
import { onMounted, ref } from 'vue'
import { getToken, parseJwt } from '@/services/authService'

const email = ref('')

onMounted(() => {
  const token = getToken()

  if (token) {
    const decoded = parseJwt(token)

    if (decoded && decoded.email) {
      email.value = decoded.email
    }
  }
})
</script>

<template>
  <div class="p-4">
    <h2>Hello {{ email }}, welcome back 👋</h2>
  </div>
</template>