<script>
const testConnection = async () => {
  try {
    const response = await fetch('/api/auth/login', {
      method: 'POST',
      headers: { 'Content-Type': 'application/json' },
      body: JSON.stringify({ email: "test@test.com", password: "123" })
    });
    console.log("Статус ответа:", response.status);
  } catch (error) {
    console.error("Ошибка связи:", error);
  }
};

</script>


<template>

  <router-view />
</template>