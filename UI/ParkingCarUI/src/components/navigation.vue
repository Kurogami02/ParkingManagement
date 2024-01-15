<template>
    <v-navigation-drawer v-model="drawer" color="rgb(9, 166, 187)">
      <template v-slot:prepend>
        <v-list-item
          lines="two"
          prepend-avatar="https://recmiennam.com/wp-content/uploads/2018/01/tai-hinh-nen-lionel-messi-dep-58.jpg"
          title="Nghĩa Đồng"
          subtitle="Logged in"
        ></v-list-item>
      </template>

      <v-divider></v-divider>

      <v-list density="compact" nav>
        <v-list-item @click="navigateTo('home')" prepend-icon="mdi-home-city" title="Home"></v-list-item>
        <v-list-item @click="navigateTo('dashboard')" prepend-icon="mdi-view-dashboard" title="Dashboard"></v-list-item>
        <v-list-item @click="navigateTo('users')" prepend-icon="mdi-account-group-outline" title="Users"></v-list-item>
        <v-list-item @click="navigateTo('account')" prepend-icon="mdi-account" title="Account"></v-list-item>
        <v-list-item @click="navigateTo('car')" prepend-icon="mdi-car" title="Cars"></v-list-item>
      </v-list>
    </v-navigation-drawer>
</template>
<script setup>

const drawer = ref(true);
const router = useRouter();

const navigateTo = (routeName) => {
  router.push({ name: routeName });
};
</script>