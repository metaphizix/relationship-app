import React, { useState } from 'react';
import { View, StyleSheet, ScrollView } from 'react-native';
import { TextInput, Button, Text, Card, HelperText } from 'react-native-paper';
import { coupleService } from '../services/coupleService';

export default function JoinCoupleScreen({ navigation }: any) {
  const [code, setCode] = useState('');
  const [loading, setLoading] = useState(false);
  const [error, setError] = useState('');

  const handleJoinCouple = async () => {
    setError('');

    if (!code) {
      setError('Please enter a couple code');
      return;
    }

    setLoading(true);
    try {
      await coupleService.joinCouple(code.toUpperCase());
      navigation.replace('Main');
    } catch (err: any) {
      setError(err.response?.data?.message || 'Failed to join couple. Check the code and try again.');
    } finally {
      setLoading(false);
    }
  };

  return (
    <ScrollView contentContainerStyle={styles.container}>
      <View style={styles.content}>
        <Text variant="headlineLarge" style={styles.title}>
          Join Your Partner
        </Text>
        <Text variant="bodyLarge" style={styles.subtitle}>
          Enter the code they shared with you
        </Text>

        <Card style={styles.card}>
          <Card.Content>
            <Text variant="bodyMedium" style={styles.cardText}>
              Your partner should have received a unique 6-character code when they created your couple.
            </Text>
          </Card.Content>
        </Card>

        <TextInput
          label="Couple Code"
          value={code}
          onChangeText={(text) => setCode(text.toUpperCase())}
          mode="outlined"
          autoCapitalize="characters"
          maxLength={6}
          style={styles.input}
          disabled={loading}
        />

        {error ? (
          <HelperText type="error" visible={!!error}>
            {error}
          </HelperText>
        ) : null}

        <Button
          mode="contained"
          onPress={handleJoinCouple}
          loading={loading}
          disabled={loading || code.length !== 6}
          style={styles.button}
        >
          Join Couple
        </Button>

        <Button
          mode="text"
          onPress={() => navigation.navigate('CreateCouple')}
          disabled={loading}
          style={styles.linkButton}
        >
          Or create a new couple
        </Button>
      </View>
    </ScrollView>
  );
}

const styles = StyleSheet.create({
  container: {
    flexGrow: 1,
    backgroundColor: '#fff',
  },
  content: {
    padding: 24,
    flex: 1,
    justifyContent: 'center',
  },
  title: {
    marginBottom: 8,
    textAlign: 'center',
  },
  subtitle: {
    marginBottom: 32,
    textAlign: 'center',
    opacity: 0.7,
  },
  card: {
    marginBottom: 24,
  },
  cardText: {
    lineHeight: 22,
  },
  input: {
    marginBottom: 8,
    fontSize: 24,
    letterSpacing: 4,
  },
  button: {
    marginTop: 16,
    paddingVertical: 6,
  },
  linkButton: {
    marginTop: 16,
  },
});
