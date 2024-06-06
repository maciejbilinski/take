import { computed, reactive } from "vue";

export const store = reactive({
    people: [],
    push(firstname, lastname){
        this.people.push({firstname, lastname})
    },
    });
export const peopleCount =  computed(() => store.people.length);