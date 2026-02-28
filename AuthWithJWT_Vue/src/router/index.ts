// import { createRouter, createWebHistory } from 'vue-router'

// const router = createRouter({
//   history: createWebHistory(import.meta.env.BASE_URL),
//   routes: [],
// })

// export default router

import { createRouter, createWebHistory } from 'vue-router'
import LoginView from '@/views/LoginView.vue'
import DashboardView from '@/views/DashboardView.vue'
import RegisterView from '@/views/RegisterView.vue'
import TransferFundsView from '@/views/TransferFundsView.vue'
import MainLayout from '@/layout/MainLayout.vue'

const router = createRouter({
  history: createWebHistory(),
  routes: [
    {
      path: '/',
      name: 'register',
      component: RegisterView,
    },
    {
      path: '/login',
      name: 'login',
      component: LoginView,
    },
    {
      path: '/dashboard',
      component: MainLayout,
      children: [
        {
          path: '',
          component: DashboardView,
        },
      ],
    },
    {
      path: '/transfer',
      component: MainLayout,
      children: [
        {
          path: '',
          component: TransferFundsView,
        },
      ],
    },
  ],
})

export default router
