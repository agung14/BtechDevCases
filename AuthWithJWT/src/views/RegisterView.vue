<template>
  <v-container fluid class="fill-height d-flex align-center justify-center bg-gradient">
    <v-card width="420" rounded="xl" elevation="12" class="pa-6">
      <!-- Header -->
      <div class="text-center mb-6">
        <h2 class="text-h5 font-weight-bold">Create Account</h2>
        <p class="text-medium-emphasis text-body-2">Sign up to continue</p>
      </div>

      <!-- Form -->
      <v-form ref="formRef" @submit.prevent="handleRegister">
        <v-text-field
          v-model="email"
          label="Email"
          variant="outlined"
          density="comfortable"
          prepend-inner-icon="mdi-email-outline"
          :rules="emailRules"
          class="mb-4"
        />

        <v-text-field
          v-model="password"
          :type="showPassword ? 'text' : 'password'"
          label="Password"
          variant="outlined"
          density="comfortable"
          prepend-inner-icon="mdi-lock-outline"
          :append-inner-icon="showPassword ? 'mdi-eye-off' : 'mdi-eye'"
          @click:append-inner="showPassword = !showPassword"
          :rules="passwordRules"
          class="mb-4"
        />

        <v-text-field
          v-model="confirmPassword"
          :type="showConfirmPassword ? 'text' : 'password'"
          label="Confirm Password"
          variant="outlined"
          density="comfortable"
          prepend-inner-icon="mdi-lock-check-outline"
          :append-inner-icon="showConfirmPassword ? 'mdi-eye-off' : 'mdi-eye'"
          @click:append-inner="showConfirmPassword = !showConfirmPassword"
          :rules="confirmPasswordRules"
          class="mb-6"
        />

        <v-btn type="submit" color="primary" size="large" block rounded="lg" :loading="loading">
          Register
        </v-btn>
      </v-form>

      <!-- Divider -->
      <v-divider class="my-6" />

      <!-- Footer -->
      <div class="text-center text-body-2">
        Already have an account?
        <v-btn variant="text" color="primary" @click="router.push('/login')"> Login </v-btn>
      </div>
    </v-card>
  </v-container>
</template>

<script setup lang="ts">
import { ref } from 'vue'
import { useRouter } from 'vue-router'

const router = useRouter()
const formRef = ref()

const email = ref('')
const password = ref('')
const confirmPassword = ref('')
const showPassword = ref(false)
const showConfirmPassword = ref(false)
const loading = ref(false)

const emailRules = [
  (v: string) => !!v || 'Email is required',
  (v: string) => /.+@.+\..+/.test(v) || 'Email must be valid',
]

const passwordRules = [
  (v: string) => !!v || 'Password is required',
  (v: string) => v.length >= 6 || 'Minimum 6 characters',
]

const confirmPasswordRules = [
  (v: string) => !!v || 'Confirm password is required',
  (v: string) => v === password.value || 'Password does not match',
]

const handleRegister = async () => {
  const { valid } = await formRef.value.validate()

  if (!valid) return

  loading.value = true

  setTimeout(() => {
    loading.value = false
    router.push({
      path: '/login',
      query: { registered: 'true' },
    })
  }, 1000)
}
</script>

<style scoped>
.bg-gradient {
  background: linear-gradient(135deg, #6366f1, #8b5cf6);
}
</style>
