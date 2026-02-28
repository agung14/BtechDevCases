<template>
  <v-card width="500" class="pa-6" rounded="xl" elevation="6">
    <h2 class="text-h6 mb-4">Transfer Funds</h2>

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
import { ref } from 'vue'

const recipient = ref('')
const amount = ref('')

const handleTransfer = () => {
  console.log('Transfer to:', recipient.value, 'Amount:', amount.value)
}
</script>