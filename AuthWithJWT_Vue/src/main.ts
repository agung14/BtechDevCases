import { createApp } from 'vue'
import App from './App.vue'
import router from './router'

import 'vuetify/styles'
import { createVuetify } from 'vuetify'
import * as components from 'vuetify/components'
import * as directives from 'vuetify/directives'
import '@mdi/font/css/materialdesignicons.css'
import { startAutoLogout } from '@/services/authService'

const vuetify = createVuetify({
  components,
  directives,
})

const token = localStorage.getItem('token')
if (token) {
  startAutoLogout()
}

createApp(App).use(router).use(vuetify).mount('#app')
