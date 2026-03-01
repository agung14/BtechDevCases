<template>
  <v-card width="500" class="pa-6" rounded="xl" elevation="6">
    <h2 class="text-h6 mb-4">Transfer Funds</h2>

    <!-- Account Info -->
    <v-alert type="info" variant="tonal" class="mb-4">
      <div><strong>Account Number:</strong> {{ account }}</div>
      <div><strong>Balance:</strong> {{ formattedBalance }}</div>
    </v-alert>

    <v-form @submit.prevent="handleTransfer">
      <v-text-field
        v-model="recipient"
        label="Recipient Account"
        variant="outlined"
        class="mb-4"
      />

      <v-text-field
        v-model="amount"
        label="Amount"
        type="number"
        variant="outlined"
        class="mb-4"
      />

      <v-btn type="submit" color="primary" block>
        Send
      </v-btn>
    </v-form>
  </v-card>
</template>

<script setup lang="ts">
import { onMounted, ref, computed } from 'vue'
import { getToken, parseJwt } from '@/services/authService'
import axiosInstance from '@/services/axiosInstance'

const recipient = ref('')
const amount = ref('')

const account = ref('')
const balance = ref<number>(0)

onMounted(() => {
  const token = getToken()

  if (!token) return

  const decoded = parseJwt(token)

  if (!decoded) return

  account.value = decoded.account ?? ''
  balance.value = Number(decoded.balance ?? 0)
})

// Format balance jadi currency
const formattedBalance = computed(() =>
  new Intl.NumberFormat('id-ID', {
    style: 'currency',
    currency: 'IDR'
  }).format(balance.value)
)

const handleTransfer = async () => {
  if (!recipient.value || !amount.value) {
    alert('Recipient and amount are required')
    return
  }

  if (Number(amount.value) <= 0) {
    alert('Amount must be greater than zero')
    return
  }

  if (Number(amount.value) > balance.value) {
    alert('Insufficient balance')
    return
  }

  try {
    const response = await axiosInstance.post('/transactions/transfer', {
      recipientAccount: recipient.value,
      amount: Number(amount.value),
    })

    // Update balance dari response backend (lebih aman)
    if (response.data.senderBalance !== undefined) {
      balance.value = response.data.senderBalance
    } else {
      // fallback kalau backend tidak kirim balance
      balance.value -= Number(amount.value)
    }

    alert('Transfer successful')

    // Reset form
    recipient.value = ''
    amount.value = ''

  } catch (error: any) {
    alert(error.response?.data || 'Transfer failed')
  }
}
</script>