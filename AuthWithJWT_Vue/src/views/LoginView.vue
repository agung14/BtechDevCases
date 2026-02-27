<template>
  <v-container fluid class="login-wrapper">
    <v-row align="center" justify="center" class="fill-height">
      <v-col cols="12" sm="10" md="6" lg="4">
        <v-card class="pa-6 login-card" elevation="12" rounded="xl">
          <div class="text-center mb-6">
            <v-icon size="48" color="primary">mdi-shield-lock</v-icon>
            <h2 class="mt-3 font-weight-bold">Welcome Back</h2>
            <p class="text-medium-emphasis">Sign in to continue</p>
          </div>

          <v-form @submit.prevent="handleLogin">
            <v-text-field
              v-model="email"
              label="Email"
              type="email"
              variant="outlined"
              prepend-inner-icon="mdi-email-outline"
              :error-messages="emailError"
              class="mb-4"
              required
            />

            <v-text-field
              v-model="password"
              :type="showPassword ? 'text' : 'password'"
              label="Password"
              variant="outlined"
              prepend-inner-icon="mdi-lock-outline"
              :append-inner-icon="showPassword ? 'mdi-eye-off' : 'mdi-eye'"
              @click:append-inner="showPassword = !showPassword"
              :error-messages="passwordError"
              class="mb-4"
              required
            />

            <v-btn
              block
              size="large"
              color="primary"
              class="mt-2"
              :loading="isLoading"
              type="submit"
              rounded="lg"
            >
              Sign In
            </v-btn>

            <v-divider class="my-6"></v-divider>

            <div class="text-center">
              <span class="text-medium-emphasis"> Don’t have an account? </span>

              <v-btn variant="text" color="primary" class="ml-1" @click="goToRegister">
                Create Account
              </v-btn>
            </div>
          </v-form>

          <v-alert v-if="errorMessage" type="error" variant="tonal" density="compact" class="mt-4">
            {{ errorMessage }}
          </v-alert>
        </v-card>
      </v-col>
    </v-row>
  </v-container>

  <v-snackbar v-model="showSnackbar" color="success" timeout="5000" location="top right">
    Registration successful. Please login.
  </v-snackbar>
</template>

<script setup lang="ts">
import { ref, onMounted } from 'vue'
import { useRouter } from 'vue-router'
import { useRoute } from 'vue-router'

const router = useRouter()
const route = useRoute()

const email = ref<string>('')
const password = ref<string>('')

const showPassword = ref<boolean>(false)

const emailError = ref<string>('')
const passwordError = ref<string>('')
const errorMessage = ref<string>('')

const isLoading = ref<boolean>(false)

const showSnackbar = ref(false)

onMounted(() => {
  if (route.query.registered === 'true') {
    showSnackbar.value = true
  }
})

const validateForm = (): boolean => {
  emailError.value = ''
  passwordError.value = ''

  let isValid = true

  if (!email.value) {
    emailError.value = 'Email is required'
    isValid = false
  }

  if (!password.value) {
    passwordError.value = 'Password is required'
    isValid = false
  }

  return isValid
}

const handleLogin = async (): Promise<void> => {
  if (!validateForm()) return

  isLoading.value = true
  errorMessage.value = ''

  setTimeout(() => {
    if (email.value === 'admin@gmail.com' && password.value === '123456') {
      router.push('/dashboard')
    } else {
      errorMessage.value = 'Invalid email or password'
    }
    isLoading.value = false
  }, 1200)
}

const goToRegister = (): void => {
  router.push('/')
}
</script>

<style scoped>
.login-wrapper {
  height: 100vh;
  background: linear-gradient(135deg, #1e3c72, #2a5298);
}

.login-card {
  backdrop-filter: blur(10px);
  border-radius: 20px;
}
</style>
