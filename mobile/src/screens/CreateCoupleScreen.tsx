import React, { useState } from 'react';
import { View, StyleSheet, ScrollView } from 'react-native';
import { Button, Text, Card, ActivityIndicator } from 'react-native-paper';
import { coupleService } from '../services/coupleService';

export default function CreateCoupleScreen({ navigation }: any) {
  const [loading, setLoading] = useState(false);
  const [coupleCode, setCoupleCode] = useState('');
  const [error, setError] = useState('');

  const handleCreateCouple = async () => {
    setLoading(true);
    setError('');

    try {
      const couple = await coupleService.createCouple();
      setCoupleCode(couple.code);
    } catch (err: any) {
      setError(err.response?.data?.message || 'Failed to create couple');
    } finally {
      setLoading(false);
    }
  };

  const handleContinue = () => {
    navigation.replace('Main');
  };

  return (
    <ScrollView contentContainerStyle={styles.container}>
      <View style={styles.content}>
        <Text variant="headlineLarge" style={styles.title}>
          Create Your Couple
        </Text>
        <Text variant="bodyLarge" style={styles.subtitle}>
          Start your journey together
        </Text>

        {!coupleCode ? (
          <>
            <Card style={styles.card}>
              <Card.Content>
                <Text variant="bodyMedium" style={styles.cardText}>
                  Create a couple to start sharing moments, tracking moods, and growing together.
                </Text>
                <Text variant="bodyMedium" style={styles.cardText}>
                  You'll receive a unique code that your partner can use to join.
                </Text>
              </Card.Content>
            </Card>

            {error ? (
              <Text style={styles.error}>{error}</Text>
            ) : null}

            <Button
              mode="contained"
              onPress={handleCreateCouple}
              loading={loading}
              disabled={loading}
              style={styles.button}
            >
              Create Couple
            </Button>

            <Button
              mode="text"
              onPress={() => navigation.navigate('JoinCouple')}
              disabled={loading}
              style={styles.linkButton}
            >
              Or join an existing couple
            </Button>
          </>
        ) : (
          <>
            <Card style={styles.successCard}>
              <Card.Content>
                <Text variant="titleLarge" style={styles.successTitle}>
                  Couple Created! 🎉
                </Text>
                <Text variant="bodyLarge" style={styles.codeLabel}>
                  Share this code with your partner:
                </Text>
                <Text variant="displaySmall" style={styles.code}>
                  {coupleCode}
                </Text>
                <Text variant="bodySmall" style={styles.codeHelp}>
                  They can use this code to join your couple
                </Text>
              </Card.Content>
            </Card>

            <Button
              mode="contained"
              onPress={handleContinue}
              style={styles.button}
            >
              Continue to App
            </Button>
          </>
        )}
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
    marginBottom: 12,
    lineHeight: 22,
  },
  successCard: {
    marginBottom: 24,
    backgroundColor: '#e8f5e9',
  },
  successTitle: {
    marginBottom: 16,
    textAlign: 'center',
    color: '#2e7d32',
  },
  codeLabel: {
    marginBottom: 8,
    textAlign: 'center',
  },
  code: {
    textAlign: 'center',
    fontWeight: 'bold',
    letterSpacing: 4,
    marginVertical: 16,
    color: '#1976d2',
  },
  codeHelp: {
    textAlign: 'center',
    opacity: 0.7,
  },
  error: {
    color: '#d32f2f',
    marginBottom: 16,
    textAlign: 'center',
  },
  button: {
    paddingVertical: 6,
  },
  linkButton: {
    marginTop: 16,
  },
});
