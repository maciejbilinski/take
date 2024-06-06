<script setup>
import { ref } from 'vue';
import { store } from '../store.js';
import { useRouter } from 'vue-router';

defineProps({
    header: String
})

const newPersonfirstname = ref('');
const newPersonLastname = ref('');

const router = useRouter();

function submit(){
    store.push(newPersonfirstname.value, newPersonLastname.value);

    router.push({name: 'list'})
}

</script>
<template>
    <h1> {{ header }}</h1>
    <form @submit.prevent="submit">
        <input type="text" v-model="newPersonfirstname" placeholder="Firstname"/>
        <br />
        <input type="text" v-model="newPersonLastname" placeholder="Lastname"/>
        <br />
        <input type="submit" value="Add new person" />
    </form>
</template>