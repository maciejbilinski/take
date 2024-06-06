<script setup>
import { store, peopleCount } from '../store.js';

defineProps({
    header: String
})

</script>
<template>
    <h1> {{ header }}</h1>
    <ul>
        <li v-for="person in store.people">{{ person.firstname }} {{ person.lastname }}</li>
    </ul>
    <br />
    <p>Number of people: {{ peopleCount }}</p>
</template>