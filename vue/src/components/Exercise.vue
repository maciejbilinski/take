<script setup>
import { computed, ref } from 'vue';

    const newPersonfirstname = ref('');
    const newPersonLastname = ref('');
    const people = ref([]);
    const peopleCount = computed(() => people.value.length);

    function push() {
        people.value.push({
            firstname: newPersonfirstname.value,
            lastname: newPersonLastname.value
        });
        newPersonfirstname.value = '';
        newPersonLastname.value = '';
    }
    

</script>

<template>
    <h2>Add new person</h2>
    <form @submit.prevent="push">
        <input type="text" v-model="newPersonfirstname" placeholder="Firstname"/>
        <br />
        <input type="text" v-model="newPersonLastname" placeholder="Lastname"/>
        <br />
        <input type="submit" value="Add new person" />
    </form>
    <br />
    <h2>
        People
    </h2>
    <ul>
        <li v-for="person in people">{{ person.firstname }} {{ person.lastname }}</li>
    </ul>
    <br />
    <p>Number of people: {{ peopleCount }}</p>
</template>
